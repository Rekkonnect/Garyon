#!/usr/bin/env python3
from __future__ import annotations

from dataclasses import dataclass, field
from pathlib import Path
import re
import sys


@dataclass
class TocNode:
    name: str
    href: str | None = None
    items: list["TocNode"] = field(default_factory=list)


NAME_RE = re.compile(r"^(?P<indent>\s*)- name:\s*(?P<name>.+?)\s*$")
HREF_RE = re.compile(r"^\s*href:\s*(?P<href>.+?)\s*$")


def parse_conceptual_toc(path: Path) -> list[TocNode]:
    lines = path.read_text(encoding="utf-8").splitlines()
    root: list[TocNode] = []
    stack: list[tuple[int, TocNode]] = []

    for line in lines:
        m = NAME_RE.match(line)
        if m:
            indent = len(m.group("indent"))
            node = TocNode(name=m.group("name").strip())

            while stack and stack[-1][0] >= indent:
                stack.pop()

            if stack:
                stack[-1][1].items.append(node)
            else:
                root.append(node)

            stack.append((indent, node))
            continue

        m = HREF_RE.match(line)
        if m and stack:
            stack[-1][1].href = m.group("href").strip()

    return root


def find_by_href(nodes: list[TocNode], href: str) -> TocNode | None:
    for node in nodes:
        if node.href == href:
            return node
        found = find_by_href(node.items, href)
        if found:
            return found
    return None


def replace_generated_block(path: Path, block_name: str, content: str) -> None:
    start = f"<!-- BEGIN GENERATED: {block_name} -->"
    end = f"<!-- END GENERATED: {block_name} -->"
    raw = path.read_text(encoding="utf-8")

    start_i = raw.find(start)
    end_i = raw.find(end)
    if start_i < 0 or end_i < 0 or end_i < start_i:
        raise RuntimeError(f"Generated block '{block_name}' not found in '{path}'.")

    before = raw[: start_i + len(start)]
    after = raw[end_i:]

    new_raw = (before + "\n" + content.rstrip() + "\n" + after).rstrip() + "\n"
    path.write_text(new_raw, encoding="utf-8", newline="\n")


def md_link(name: str, href: str) -> str:
    return f"- [{name}]({href})"


def render_child_links(children: list[TocNode]) -> str:
    lines: list[str] = []
    for child in children:
        if child.href:
            lines.append(md_link(child.name, child.href))
    return "\n".join(lines)


def main(repo_root: Path) -> int:
    docs_dir = repo_root / "docs"
    docs_toc = parse_conceptual_toc(docs_dir / "toc.yml")
    guides_toc = parse_conceptual_toc(docs_dir / "guides" / "toc.yml")

    # Update guide parent pages.
    for parent in ["getting-started.md", "extensions-utilities.md", "advanced-features.md"]:
        node = find_by_href(guides_toc, parent)
        if not node:
            raise RuntimeError(f"Could not find node for guides/{parent} in guides TOC.")
        replace_generated_block(docs_dir / "guides" / parent, "child-links", render_child_links(node.items))

    # Update guides index.
    guides_index_lines: list[str] = []
    for section in guides_toc:
        if section.href == "index.md" or not section.href:
            continue
        guides_index_lines.append(f"## [{section.name}]({section.href})")
        guides_index_lines.append("")
        for child in section.items:
            if child.href:
                guides_index_lines.append(md_link(child.name, child.href))
        guides_index_lines.append("")

    replace_generated_block(docs_dir / "guides" / "index.md", "guides-index", "\n".join(guides_index_lines).rstrip())

    # Update docs home index.
    getting_started_lines: list[str] = []
    for href in ["guides/installation.md", "guides/quick-start.md", "QUICK_REFERENCE.md", "api/index.md"]:
        node = find_by_href(docs_toc, href)
        if node and node.href:
            getting_started_lines.append(md_link(node.name, node.href))

    user_guides = find_by_href(docs_toc, "guides/index.md")
    browse_lines: list[str] = []
    if user_guides and user_guides.href:
        browse_lines.append(md_link(user_guides.name, user_guides.href))
        def render_tree(items: list[TocNode], depth: int) -> None:
            for item in items:
                if not item.href:
                    continue
                browse_lines.append(("  " * depth) + md_link(item.name, item.href))
                if item.items:
                    render_tree(item.items, depth + 1)
        render_tree(user_guides.items, 1)

    home_lines = [
        "## Getting Started",
        "",
        *getting_started_lines,
        "",
        "## Browse",
        "",
        *browse_lines,
    ]
    replace_generated_block(docs_dir / "index.md", "home-index", "\n".join(home_lines).rstrip())

    return 0


if __name__ == "__main__":
    try:
        root = Path(__file__).resolve().parent.parent
        raise SystemExit(main(root))
    except Exception as ex:
        print(f"[generate-nav] {ex}", file=sys.stderr)
        raise

