var image;
var i = 1;
var errorCount = 1;
var size = "medium";
function reload_img() {
    image.src = "/helloimage/" + i++ + "/" + size;
}
function error_img() {
    setTimeout("image.src = '/helloimage/error'" + errorCount++ + "/" + size, 100);
}
function init_img(size) {
    this.size = size;
    image = document.getElementById("jpeg_img");
    image.onload = reload_img;
    image.onerror = error_img;
    reload_img();
}