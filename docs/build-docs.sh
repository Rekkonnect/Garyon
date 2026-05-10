#!/bin/bash

set -euo pipefail

clear_api_output() {
    local api_dir="$script_dir/api"
    local index_path="$api_dir/index.md"

    mkdir -p "$api_dir"

    if [[ -d "$api_dir" ]]; then
        while IFS= read -r -d '' file; do
            if [[ "$file" != "$index_path" ]]; then
                rm -f -- "$file"
            fi
        done < <(find "$api_dir" -type f -print0)

        while IFS= read -r -d '' dir; do
            rmdir --ignore-fail-on-non-empty -- "$dir" 2>/dev/null || true
        done < <(find "$api_dir" -type d -print0 | sort -rz)
    fi
}

set_api_toc_root() {
    local api_toc_path="$script_dir/api/toc.yml"
    local temp_file

    if [[ ! -f "$api_toc_path" ]]; then
        echo "Generated API TOC was not found at '$api_toc_path'." >&2
        exit 1
    fi

    if grep -Fqx -- '- name: API Reference' "$api_toc_path"; then
        return
    fi

    temp_file="$(mktemp)"

    {
        printf '### YamlMime:TableOfContent\nitems:\n'
        printf '%s\n' '- name: API Reference' '  href: index.md' '  items:'
        awk '
            BEGIN { skipping = 1 }
            skipping && /^items:$/ { skipping = 0; next }
            skipping { next }
            { print "  " $0 }
        ' "$api_toc_path"
    } > "$temp_file"

    mv "$temp_file" "$api_toc_path"
}

set_api_landing_page() {
    local api_toc_path="$script_dir/api/toc.yml"
    local api_index_path="$script_dir/api/index.md"

    if [[ ! -f "$api_toc_path" ]]; then
        echo "Generated API TOC was not found at '$api_toc_path'." >&2
        exit 1
    fi

    {
        printf '# API Reference\n\n'
        printf 'Browse the generated Garyon API reference by namespace.\n\n'
        printf 'Use the search box or the namespace list below to jump directly into the API surface.\n\n'
        printf '## Namespaces\n\n'
        awk '
            /^  - uid: / {
                uid = substr($0, 10)
                name = uid
                if (getline > 0 && $0 ~ /^    name: /) {
                    name = substr($0, 11)
                }
                printf "- [%s](%s.yml)\n", name, uid
            }
        ' "$api_toc_path"
        printf '\nThe namespace pages include the local API table of contents for drilling into individual types.\n'
    } > "$api_index_path"
}

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" >/dev/null 2>&1 && pwd)"
cd "$script_dir"

echo "Building Garyon documentation..."

if ! command -v docfx >/dev/null 2>&1; then
    echo "DocFX is not installed. Installing..."
    dotnet tool install -g docfx
fi

echo "Generating API metadata..."
echo "Cleaning previous API output..."
clear_api_output
docfx metadata docfx.json

echo "Normalizing API table of contents..."
set_api_toc_root

echo "Generating API landing page..."
set_api_landing_page

echo "Building documentation site..."
docfx build docfx.json

echo "Documentation built successfully."
echo "Output location: docs/_site/"

if [[ "${1:-}" == "--serve" || "${1:-}" == "-s" ]]; then
    echo "Starting documentation server on http://localhost:8080 ..."
    docfx docfx.json --serve
fi
