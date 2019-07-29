import React from 'react';
import ReactDOM from 'react-dom';
import 'jsdom-global/register';
import should from 'should';
import { expect } from 'chai';
import sinon from 'sinon';
import { configure, shallow} from 'enzyme';

import Adapter from 'enzyme-adapter-react-16';
configure({ adapter: new Adapter() });

import Modal from '../components/Modal/ModalComponent';

import { Dialog } from '@material-ui/core';


describe('<Modal/>', () => {
    it('should have props for open and handleClose ', () => {
        const open = false;
        const handleClickClose = sinon.spy();
        const wrapper = shallow(<Modal
            open={open}
            handleClose={handleClickClose}
        />);
        expect(wrapper.find(Dialog).prop('open')).to.equal(false);
        expect(wrapper.find(Dialog).prop('handleClose')).to.equal(handleClickClose());
    });
})