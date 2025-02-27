//$(".gambar").attr("src", "");
var $uploadCrop,
    tempFilename,
    rawImg,
    imageId;

function readFile(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('.upload-demo').addClass('ready');
            $('#cropImagePop').modal('show');
            rawImg = e.target.result;
        };
        reader.readAsDataURL(input.files[0]);
    }
    else {
        //swal("Sorry - you're browser doesn't support the FileReader API");
    }
}


$uploadCrop = $('#upload-demo').croppie({
    viewport: {
        width: 300,
        height: 350
    },
    boundary: {
        width: 400,
        height: 400
    },
    showZoomer: true,
    enableOrientation: true,
    enforceBoundary: true,
    enableExif: true
});

$('#cropImagePop').on('shown.bs.modal', function () {
    $uploadCrop.croppie('bind', {
        url: rawImg
    }).then(function () {
        //console.log('Photo Bind Complete');
    });
});

$('.item-img').on('change', function () {
    imageId = $(this).data('id'); tempFilename = $(this).val();
    $('#cancelCropBtn').data('id', imageId); readFile(this);
});

$('#cropImageBtn').on('click', function (ev) {
    var typeId = $('#ImageTypeId').val();
    $uploadCrop.croppie('result', {
        type: 'base64',
        format: 'png',
        size: { width: 300, height: 300 }
    }).then(function (resp) {
        $('#photoPicture').attr('src', resp);
        $('#cropImagePop').modal('hide');
        $('#ImageTypeId').val(1);
    });
    $('.item-img').val('');
});

$('#btnRoute').on('click', function () {
    $uploadCrop.croppie('rotate', parseInt($(this).data('deg')));
});



var $uploadCropsign,
    tempFilenamesign,
    rawImgsign,
    imageIdsign;
function readFilesign(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('.upload-demosign').addClass('ready');
            $('#cropImagePopsign').modal('show');
            rawImgsign = e.target.result;
        };
        reader.readAsDataURL(input.files[0]);
    }
    else {
        //swal("Sorry - you're browser doesn't support the FileReader API");
    }
}




$uploadCropsign = $('#upload-demosign').croppie({
    viewport: {
        width: 300,
        height: 80
    },
    showZoomer: true,
    enableOrientation: true,
    enforceBoundary: true,
    enableExif: true
});
$('#cropImagePopsign').on('shown.bs.modal', function () {
    $uploadCropsign.croppie('bind', {
        url: rawImgsign
    }).then(function () {
        //$('.cr-slider').attr({ 'min': 0.6500, 'max': 1.5000 });
        //console.log('Sign bind complete');
    });
});

$('.item-imgsign').on('change', function () {
    imageIdsign = $(this).data('id'); tempFilenamesign = $(this).val();
    $('#cancelCropBtnsign').data('id', imageIdsign); readFilesign(this);
});
$('#cropImageBtnsign').on('click', function (ev) {

    $uploadCropsign.croppie('result', {
        type: 'base64',
        format: 'png',
        size: { width: 300, height: 300 }
    }).then(function (resp) {
        $('#picture').attr('src', resp);
        $('#cropImagePopsign').modal('hide');
        $('#ImageTypeId').val(2);
    });
    $('.item-imgsign').val('');
});

$('#btnRouteSign').on('click', function () {
    $uploadCropsign.croppie('rotate', parseInt($(this).data('deg')));
    //$uploadCrop.croppie('rotate', 90);
});