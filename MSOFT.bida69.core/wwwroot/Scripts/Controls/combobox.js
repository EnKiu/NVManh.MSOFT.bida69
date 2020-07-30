class Combobox {
    constructor() {

    }
    static buildComboboxElement(el) {
        var getDataUrl = el.getAttribute("GetDataUrl");
        var fieldValue = el.getAttribute("FieldValue");
        var fieldDisplay = el.getAttribute("FieldDisplay");
        var label = el.getAttribute("Label");
        var autoLoadData = eval(el.getAttribute("AutoLoadData"));
        var showOptionAll = eval(el.getAttribute("ShowOptionAll"));
        var id = el.id;
        var comboboxDataHTML = $('<select id="{0}" class="form-control" combobox-data dataindex="{1}"></select>'.format(id, fieldValue));
        comboboxDataHTML.data("FieldValue", fieldValue);
        if (label) {
            $(el).before('<label class="lbl-custom">{0}: </label>'.format(label));
        }
        if (showOptionAll) {
            comboboxDataHTML.append('<option selected>Chọn {0}...</option>'.format(label.toLowerCase()))
        }
        if (getDataUrl && autoLoadData) {
            ajaxJSON.get(getDataUrl, {}, true, function (data) {
                $.each(data, function (index, item) {
                    var optionHTML = '<option value="{0}">{1}</option>'.format(item[fieldValue], item[fieldDisplay]);
                    comboboxDataHTML.append(optionHTML);
                })
            })
        }
        $(el).replaceWith(comboboxDataHTML);
    }
}
