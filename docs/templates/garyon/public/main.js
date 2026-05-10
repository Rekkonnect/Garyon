export default {
  start: () => {
    const isEditableTarget = (target) => {
      if (!(target instanceof HTMLElement)) {
        return false;
      }

      return target.isContentEditable ||
        target instanceof HTMLInputElement ||
        target instanceof HTMLTextAreaElement ||
        target instanceof HTMLSelectElement;
    };

    const focusSearch = () => {
      const searchInput = document.querySelector('#search-query');

      if (!(searchInput instanceof HTMLInputElement)) {
        return false;
      }

      searchInput.disabled = false;
      searchInput.focus();
      searchInput.select();
      return true;
    };

    const fixContextualValueKeyword = () => {
      for (const element of document.querySelectorAll('span.hljs-keyword')) {
        if (element.textContent === 'value') {
          element.classList.remove('hljs-keyword');
        }
      }
    };

    document.addEventListener('keydown', (event) => {
      const pressedSearchShortcut = (event.ctrlKey || event.metaKey) && !event.shiftKey && !event.altKey &&
        (event.key.toLowerCase() === 'k' || event.key.toLowerCase() === 'o');

      if (!pressedSearchShortcut || isEditableTarget(event.target)) {
        return;
      }

      if (focusSearch()) {
        event.preventDefault();
      }
    });

    fixContextualValueKeyword();

    const observer = new MutationObserver(() => fixContextualValueKeyword());
    observer.observe(document.body, { subtree: true, childList: true });
  },
};