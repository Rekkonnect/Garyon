export default {
  start: () => {
    // Highlight.js treats C# contextual keyword `value` as a keyword everywhere,
    // which is visually noisy in docs. Downgrade it back to plain text.
    const fixContextualValueKeyword = () => {
      for (const el of document.querySelectorAll('span.hljs-keyword')) {
        if (el.textContent === 'value') {
          el.classList.remove('hljs-keyword');
        }
      }
    };

    fixContextualValueKeyword();

    // Re-apply when navigating (SPA-style) or when content is injected.
    const observer = new MutationObserver(() => fixContextualValueKeyword());
    observer.observe(document.body, { subtree: true, childList: true });
  },
};

