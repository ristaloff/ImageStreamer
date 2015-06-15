var image;
var i = 1;
var errorCount = 1;
function reload_img() {
    image.src = "/helloimage/" + i++;

}
function error_img() {
    setTimeout("image.src = '/helloimage/error'" + errorCount++, 100);
}
function init_img() {
    image = document.getElementById("jpeg_img");
    image.onload = reload_img;
    image.onerror = error_img;
    reload_img();
}