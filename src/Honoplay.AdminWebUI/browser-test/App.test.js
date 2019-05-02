import should from 'should';
import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';


describe("App.jsx", () => {
    it("should be true", () => {
        "1".should.be.exactly("1");
    });

    it('renders without crashing', () => {
        const div = document.createElement('div');
        ReactDOM.render("<App />", div);
    });
});


