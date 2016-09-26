function CandidatesViewModel(candidates) {
    var self = this;

    this.candidates = ko.observableArray(candidates);
    this.deleteCandidate = function(candidate){
        self.candidates.remove(candidate);
        $.ajax({
            url: "api/candidate/" + candidate.Id,
            type: "DELETE"
        })
    }
    this.addCandidate = function () {
        var candidate = { Id : 0, FirstName: "", LastName: "", Email: "", Comment: "", Created: "" }
        $.ajax({
            url: "api/candidate",
            type: "POST",
            data: JSON.stringify(candidate),
            dataType: "json",
            contentType: "application/json",
            success: function (id) {
                candidate.Id = id;
                self.candidates.push(candidate)
            }
        });
    }
    this.save = function(){
        $.ajax({
            url: "api/candidate",
            type: "POST",
            data: JSON.stringify(this),
            dataType: "json",
            contentType: "application/json"
        });
    }
}
$(function () {
    $.get("/api/candidate", function (data) {
        ko.applyBindings(new CandidatesViewModel(data));
    });
})