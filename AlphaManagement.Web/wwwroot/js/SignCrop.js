//$(".gambar").attr("src", "");
var $uploadCropsign,
    tempFilenamesign,
    rawImgsign,
    imageIdsign;
function readFile(input) {
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
        swal("Sorry - you're browser doesn't support the FileReader API");
    }
}

$uploadCropsign = $('#upload-demosign').croppie({
    viewport: {
        width: 300,
        height: 80
    },
    enforceBoundary: false,
    enableExif: true
});
$('#cropImagePopsign').on('shown.bs.modal', function () {
     alert('Shown pop');
    $uploadCropsign.croppie('bind', {
        url: rawImgsign
    }).then(function () {
        console.log('Sign bind complete');
    });
});

$('.item-imgsign').on('change', function () {
    imageIdsign = $(this).data('id'); tempFilenamesign = $(this).val();
    $('#cancelCropBtnsign').data('id', imageIdsign); readFile(this);
});
$('#cropImageBtnsign').on('click', function (ev) {

    $uploadCropsign.croppie('result', {
        type: 'base64',
        format: 'jpeg',
        size: { width: 300, height: 300 }
    }).then(function (resp) {
        $('#picture').attr('src', resp);
        $('#cropImagePopsign').modal('hide');
    });

});