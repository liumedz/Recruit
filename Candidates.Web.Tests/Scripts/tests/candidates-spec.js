/// <reference path="../libs/jquery-3.1.1.js" />
/// <reference path="../libs/knockout-3.4.0.js" />
/// <reference path="../libs/bootstrap.js" />
/// <reference path="../dependencies/candidates.js" />

describe('Candidates', function () {

    var service;
    beforeEach(function () {
        service = jasmine.createSpyObj('service', ['save', 'get', 'delete']);
    });

    describe('#save', function () {
        it('should save candidate', function () {
            var candidateViewModel = new app.candidatesViewModel(ko, service);
            candidateViewModel.save();
            expect(service.save).toHaveBeenCalled();
            expect(service.get).toHaveBeenCalled();
            expect(service.delete.calls.any()).toBe(false);
        });
    });
    describe('#delete', function () {
        it('should delete candidate', function () {
            var candidate = { id: 1 };
            service.get.and.callFake(function (candidates) {
                candidates.push(candidate);
            });
            var candidateViewModel = new app.candidatesViewModel(ko, service);
            expect(candidateViewModel.candidates().length).toBe(1);
            candidateViewModel.delete(candidate);
            expect(service.get).toHaveBeenCalled();
            expect(service.delete).toHaveBeenCalled();
            expect(service.save.calls.any()).toBe(false);
            expect(candidateViewModel.candidates().length).toBe(0);
        });
    });
});
