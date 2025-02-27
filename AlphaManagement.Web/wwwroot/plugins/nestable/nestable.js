
/**
* Theme: Velonic Admin Template
* Author: Coderthemes
* Nestable Component
*/

!function($) {
    "use strict";

    var Nestable = function() {};

    Nestable.prototype.updateOutput = function (e) {
        var list = e.length ? e : $(e.target),
            output = list.data('output');
        if (window.JSON) {
            //output.val(window.JSON.stringify(list.nestable('serialize'))); //, null, 2));
        } else {
            output.val('JSON browser support required for this demo.');
        }
    },
    //init
    Nestable.prototype.init = function() {
        // activate Nestable for list 1
        $('.nestable_list_1').nestable({
            group: 1
        }).on('change', this.updateOutput);

        // activate Nestable for list 2
        $('.nestable_list_2').nestable({
            group: 1
        }).on('change', this.updateOutput);

        // output initial serialised data
        this.updateOutput($('.nestable_list_1').data('output', $('#nestable_list_1_output')));
        this.updateOutput($('.nestable_list_2').data('output', $('#nestable_list_2_output')));
        
        $('#nestable_list_menu').on('click', function (e) {
            var target = $(e.target),
                action = target.data('action');
            if (action === 'expand-all') {
                $('.dd').nestable('expandAll');
            }
            if (action === 'collapse-all') {
                $('.dd').nestable('collapseAll');
            }
        });
        
        $('.nestable_list_3').nestable();
    },
    //init
        $.Nestable = new Nestable, $.Nestable.Constructor = Nestable
}(window.jQuery),

//initializing 
function($) {
    "use strict";
    $.Nestable.init();
    var rankId = $('#rankId').val();
    var unitId = $('#unitId').val();
    var batchId = $('#batchId').val();
    var bandId = $('#bandId').val();
    var selectedStatusId = $('#selectedStatusId').val();
    var servicePeriodId = $('#servicePeriodId').val();
    if (rankId != 0) {
        $('.dd').nestable('expandAll');
    } else if (unitId != 0) {
        $('.dd').nestable('expandAll');
    } else if (batchId != 0) {
        $('.dd').nestable('expandAll');
    } else if (bandId != 0) {
        $('.dd').nestable('expandAll');
    } else if (servicePeriodId != 0) {
        $('.dd').nestable('expandAll');
    } else if (selectedStatusId != '') {
        $('.dd').nestable('expandAll');
    } else {
        $('.dd').nestable('collapseAll');
    };

    $('.clsExpandClick').on('click', function () {
        $('.dd').nestable('expandAll');
    });


}(window.jQuery);
