"use strict";

var _should = _interopRequireDefault(require("should"));

var _react = _interopRequireDefault(require("react"));

var _reactDom = _interopRequireDefault(require("react-dom"));

var _App = _interopRequireDefault(require("./App"));

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

describe("App.jsx", function () {
  it("should be true", function () {
    "1".should.be.exactly("1");
  });
  it('renders without crashing', function () {
    var div = document.createElement('div');

    _reactDom.default.render(_react.default.createElement(_App.default, null), div);
  });
});
