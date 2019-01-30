import should from 'should';
import React from 'react';
import ReactDOM from 'react-dom';
import { configure, shallow } from 'enzyme';
import App from './App.jsx';

import Adapter from 'enzyme-adapter-react-16';

configure({ adapter: new Adapter() });

describe("App.jsx", () => {
    it("should be true", () => {
        "1".should.be.exactly("1");
    });

    it('renders without crashing', () => {
        const wrapper = shallow(<App />);
        const welcome = <h1> Hello, World!! </h1>;
        wrapper.contains(welcome).should.be.true();
    });
});


