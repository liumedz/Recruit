var candidates = require('../Scripts/src/candidates.js')
describe('Candidates', function () {
    var service;
    beforeEach(function () {
        service = jasmine.createSpyObj('service', ['save', 'get', 'delete']);
    });

    describe('#save', function () {
        it('should make one ajax request', function () {

            var candidateViewModel = new candidates.app.candidatesViewModel(ko, service);
            candidateViewModel.save();
            expect(service.save).toHaveBeenCalled();
            expect(service.get).toHaveBeenCalled();
            expect(service.delete.calls.any()).toBe(false);
        });
    });
    describe('#delete', function () {
        it('should make one ajax request', function () {


            var candidateViewModel = new candidates.app.candidatesViewModel(ko, service);
            candidateViewModel.delete();
            expect(service.get).toHaveBeenCalled();
        });
    });
});
