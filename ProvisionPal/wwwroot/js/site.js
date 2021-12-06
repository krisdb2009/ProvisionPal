// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
    return new bootstrap.Popover(popoverTriggerEl)
})

var permsFilter = document.getElementById('permsFilter');
var permsList = document.getElementById('permsList');
permsFilter.onkeyup = function () {
    permsList.childNodes.forEach(function (cn) {
        if (cn.innerText == undefined) return;
        if (cn.innerText.toLowerCase().includes(permsFilter.value.toLowerCase())) {
            cn.classList.remove('d-none');
        } else {
            cn.classList.add('d-none');
        }
    });
};

var mainForm = document.getElementById('mainForm');
var permMiniList = document.getElementById('permMiniList');
var permLiTemplate = document.getElementById('permLiTemplate');
var permMiniMessage = document.getElementById('permMiniMessage');
mainForm.onchange = function () {
    permMiniList.innerHTML = "";
    permMiniMessage.classList.remove('d-none');
    permMiniList.classList.add('d-none');
    var anyChecked = false;
    permsList.childNodes.forEach(function (cn) {
        if (cn.innerText == undefined) return;
        var input = cn.getElementsByTagName('input')[0];
        if (input.checked) {
            anyChecked = true;
            var newPerm = permLiTemplate.cloneNode(true);
            newPerm.classList.remove('d-none');
            var spans = newPerm.getElementsByTagName('span');
            spans[0].innerText = cn.getElementsByTagName('h5')[0].innerText;
            spans[1].onclick = function () {
                input.checked = false;
                mainForm.onchange();
            };
            permMiniList.appendChild(newPerm);
        }
    });
    if (anyChecked) {
        permMiniMessage.classList.add('d-none');
        permMiniList.classList.remove('d-none');
    }
};
mainForm.onchange();