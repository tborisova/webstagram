var dropbox = document.getElementById('imgBG');
var mainDiv = document.getElementById('mainDiv');
var Filters = {};
arr = [];

//dropbox.style.height = "500px";
// Setup drag and drop handlers. 
dropbox.addEventListener('dragenter', stopDefault, false);
dropbox.addEventListener('dragover', stopDefault, false);
dropbox.addEventListener('dragleave', stopDefault, false);
dropbox.addEventListener('drop', onDrop, false);

function stopDefault(e) {
    e.stopPropagation();
    e.preventDefault();
}

//where do you send the image to the server?
function onDrop(e) {
    e.stopPropagation();
    e.preventDefault();

    var readFileSize = 0;
    var files = e.dataTransfer.files;


    file = files[0];
    readFileSize += file.fileSize;


    // Only process image files. 
    var imageType = /image.*/;

    if (!file.type.match(imageType)) {
        return;
    }


    var reader = new FileReader();


    reader.onerror = function (e) {
        alert('Error code: ' + e.target.error);
    };


    // Create a closure to capture the file information. 
    reader.onload = (function (aFile) {
        return function (evt) {
            dropbox.src = evt.target.result;
            dropbox.style.height = "300px";
            Filters.dropbox = dropbox;
            Generate();
        }
    })(file);

    // Read in the image file as a data url. 
    reader.readAsDataURL(file);

}