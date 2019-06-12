import React from 'react';
import ReactDOM from 'react-dom';
import 'jsdom-global/register'; 
import should from 'should';
import { expect } from 'chai';
import sinon from 'sinon';
import { configure, shallow, mount, render } from 'enzyme';
import { init } from '@omegabigdata/terasu-api-proxy';

import Adapter from 'enzyme-adapter-react-16';
configure({ adapter: new Adapter() });

import Button from '../components/Button/ButtonComponent';

import {} from '@material-ui/core';

describe('<Button/>', () => {
    it('should have props for data, modalTitle, modalInputName, handleClickClose ', () => {
        const onClick = sinon.spy();
        const wrapper = shallow(<Button
            buttonColor = "primary"
            buttonName = 'Save'
            onClick = {onClick}
        />);
        wrapper.props().buttonColor.should.be.defined;
        wrapper.props().buttonName.should.be.defined; 
        wrapper.props().onClick.should.be.defined;
    });

})