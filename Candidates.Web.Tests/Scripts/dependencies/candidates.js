"use strict";

var app = app || {};

(function (k) {


    app.candidatesViewModel = function (k, candidateService, noteService) {
        var self = this;

        self.candidates = k.observableArray();
        self.notes = k.observableArray();
        candidateService.get(self.candidates)
        self.currentCandidate;
        this.delete = function (candidate) {
            self.candidates.remove(candidate);
            if (candidate.id != 0)
                candidateService.delete(candidate);
        }
        this.add = function () {
            var candidate = { id: k.observable(0), firstName: "", lastName: "", email: "", comment: "", created: "" }
            self.candidates.push(candidate);
        }
        this.save = function () {
            candidateService.save(this);
        }

        this.openNotes = function (candidate) {
            self.currentCandidate = candidate;
            noteService.get(candidate, self.notes)
        }

        this.addNote = function (candidatesViewModel) {
            var note = { id: 0, notes: "", created: "", candidateId: candidatesViewModel.currentCandidate.id() }
            self.notes.push(note);
        }
        this.saveNote = function (note) {
            noteService.save(note);
        }
        this.deleteNote = function (note) {
            self.notes.remove(note);
            if (note.id != 0)
                noteService.delete(note);
        }
    }


    app.candidateService = function () {
        this.save = function (candidate) {
            var obj = { id: candidate.id(), firstName: candidate.firstName, lastName: candidate.lastName, email: candidate.email, comment: candidate.comment }
            $.ajax({
                url: "api/candidate",
                type: "POST",
                data: JSON.stringify(obj),
                dataType: "json",
                contentType: "application/json",
                success: function (id) {
                    candidate.id(id);
                }
            });
        }
        this.delete = function (candidate) {
            $.ajax({
                url: "api/candidate/" + candidate.id(),
                type: "DELETE"
            });
        }
        this.get = function (candidates) {
            $.get("/api/candidate", function (data) {
                data.forEach(function (candidate) {
                    candidate.id = k.observable(candidate.id)
                    candidates.push(candidate);
                });
            });
        }
    }
    app.noteService = function () {
        this.save = function (note) {
            $.ajax({
                url: "api/note",
                type: "POST",
                data: JSON.stringify(note),
                dataType: "json",
                contentType: "application/json",
                success: function (id) {
                    note.id = id;
                }
            });
        }
        this.delete = function (note) {
            $.ajax({
                url: "api/note/" + note.id,
                type: "DELETE"
            });
        }
        this.get = function (candidate, notes) {
            notes.splice(0, notes().length);
            $.get("/api/note?candidateId=" + candidate.id(), function (data) {
                data.forEach(function (note) {
                    notes.push(note);
                });
            });
        }
    }
    
    k.applyBindings(new app.candidatesViewModel(k, new app.candidateService(), new app.noteService()));

})(ko);