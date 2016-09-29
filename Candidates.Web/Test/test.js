var assert = require('assert');
var candidatesViewModel = require('../Scripts/candidates.js')
describe('Candidates', function () {
    describe('#save', function () {
        it('should make one ajax request', function () {
            var candidateServiceMock = function () {
                var self = this;
                self.saveRequestCount = 0;
                self.save = function () {
                    self.saveRequestCount++;
                };
                self.get = function (candidates) {
                };
            };
            var koMock = function () {
                this.observableArray = function () { return [{}] }
            };
            var service = new candidateServiceMock();
            var candidateViewModel = new candidatesViewModel(new koMock(), service);
            candidateViewModel.save();
            assert.equal(1, service.saveRequestCount);
        });
    });
    describe('#delete', function () {
        it('should make one ajax request', function () {
            var candidateServiceMock = function () {
                var self = this;
                self.deleteRequestCount = 0;
                self.save = function () {
                    self.deleteRequestCount++;
                };
                self.get = function (candidates) {
                };

                self.delete = function () {
                    deleteRequestCount++;
                };
            };
            var koMock = function () {
                this.observableArray = function () { return [{}] }
            };
            var service = new candidateServiceMock();
            var candidateViewModel = new candidatesViewModel(new koMock(), service);
            candidateViewModel.delete();
            assert.equal(1, service.deleteRequestCount);
        });
    });
});
