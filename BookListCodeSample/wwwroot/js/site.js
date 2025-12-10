
//scale font for book titles
window.fitText = function () {
    document.querySelectorAll('.card-title-text').forEach(function (el) {
        var parent = el.closest('.card-header');
        if (!parent) return;

        var maxWidth = parent.offsetWidth * 0.65;
        var fontSize = 20;

        el.style.fontSize = fontSize + 'px';
        el.style.whiteSpace = 'nowrap';

        while (el.scrollWidth > maxWidth && fontSize > 10) {
            fontSize--;
            el.style.fontSize = fontSize + 'px';
        }
    });
};