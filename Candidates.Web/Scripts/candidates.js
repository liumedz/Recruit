"use strict";

var app = app || {};

(function (k) {


    app.candidatesViewModel = function (k, candidateService) {
        var self = this;

        self.candidates = k.observableArray();
        candidateService.get(self.candidates)
        this.delete = function (candidate) {
            self.candidates.remove(candidate);
            if (candidate.Id != 0)
                candidateService.delete(candidate)
        }
        this.add = function () {
            var candidate = { Id: 0, FirstName: "", LastName: "", Email: "", Comment: "", Created: "" }
            self.candidates.push(candidate);
        }
        this.save = function () {
            candidateService.save(this);
        }
    }


    app.candidateService = function () {
        this.save = function (candidate) {
            $.ajax({
                url: "api/candidate",
                type: "POST",
                data: JSON.stringify(candidate),
                dataType: "json",
                contentType: "application/json",
                success: function (id) {
                    candidate.Id = id;
                }
            });
        }
        this.delete = function (candidate) {
            $.ajax({
                url: "api/candidate/" + candidate.Id,
                type: "DELETE"
            });
        }
        this.get = function (candidates) {
            $.get("/api/candidate", function (data) {
                data.forEach(function (candidate) {
                    candidates.push(candidate);
                });
            });
        }
    }
    
    k.applyBindings(new app.candidatesViewModel(k, new app.candidateService()));

})(ko);


