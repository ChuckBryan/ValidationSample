$.validator.addMethod("problemtyperequiredif",
    function(value, element, parameters) {
        var id = "#" + parameters["dependentproperty"];

        var control = $(id);
        var actualvalue = control.val();

        console.log(moment(actualvalue).isValid());

        if (!moment(actualvalue).isValid()) {
            return true;
        }

        if (moment(actualvalue).diff(moment()) < 0) {
            return element.value > 0;
        }

        return true;


    }
);

$.validator.unobtrusive.adapters.add(
    "problemtyperequiredif",
    ["dependentproperty", "targetvalue"],
    function(options) {
        options.rules["problemtyperequiredif"] = {
            dependentproperty: options.params["dependentproperty"],
            targetvalue: options.params["targetvalue"]
        };
        options.messages["problemtyperequiredif"] = options.message;
    });