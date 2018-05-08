function addElement(parentId, elementTag, elementId, html) {
    // Adds an element to the document
    var p = document.getElementById(parentId);
    var newElement = document.createElement(elementTag);
    newElement.setAttribute('id', elementId);
    newElement.innerHTML = html;
    p.appendChild(newElement);
}



var indexId = 0;

function addQuestion() {
    indexId++;
    var html = '<div class="form-group"> <label class="control-label col-md-2" for="Questions_' + indexId + '__QuestionName">Question</label> <div class="cold-md-10"> <input class="form-control text-box single-line" data-val="true" data-val-required="The Question Name field is required." id="Questions_' + indexId + '__QuestionName" name="Questions[' + indexId + '].QuestionName" type="text" value=""> <span class="field-validation-valid text-danger" data-valmsg-for="Questions[' + indexId + '].QuestionName" data-valmsg-replace="true"></span> </div> </div> <div class="form-group"> <label class="control-label col-md-2" for="Questions_' + indexId + '__Required">Required</label> <div class="col-md-10"> <input checked="checked" data-val="true" data-val-required="The Required field is required." id="Questions_' + indexId + '__Required" name="Questions[' + indexId + '].Required" type="checkbox" value="true"><input name="Questions[' + indexId + '].Required" type="hidden" value="false"> </div> </div>';
    addElement('questionGroup', 'div', 'newQuestion', html);
}


function addQuestionForEditPage() {
    var count = document.getElementById("questionGroup").getAttribute("count");
    count++;
    var html = '<div class="form-group"> <label class="control-label col-md-2" for="Questions_' + count + '__QuestionName">Question</label> <div class="cold-md-10"> <input class="form-control text-box single-line" data-val="true" data-val-required="The Question Name field is required." id="Questions_' + count + '__QuestionName" name="Questions[' + count + '].QuestionName" type="text" value=""> <span class="field-validation-valid text-danger" data-valmsg-for="Questions[' + count + '].QuestionName" data-valmsg-replace="true"></span> </div> </div> <div class="form-group"> <label class="control-label col-md-2" for="Questions_' + count + '__Required">Required</label> <div class="col-md-10"> <input checked="checked" data-val="true" data-val-required="The Required field is required." id="Questions_' + count + '__Required" name="Questions[' + count + '].Required" type="checkbox" value="true"><input name="Questions[' + count + '].Required" type="hidden" value="false"> </div> </div>';
    addElement('questionGroup', 'div', 'newQuestion', html);
}


    
