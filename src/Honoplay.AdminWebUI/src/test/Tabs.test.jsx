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

import Tabs from '../components/Tabs/FullWidthTabs';
import Snackbar from '../components/Tabs/SnackbarContextComponent';

import { Button, IconButton } from '@material-ui/core';

describe('<Tabs/>', () => {
    it('should have disabled tab2 and tab3 when opened tenant information', () => {
        const wrapper = shallow(<Tabs/>).dive();
        expect(wrapper.state().disabled).to.equal(true);
        expect(wrapper.state().disabled2).to.equal(true);
    });

    it('should disable the button and appear loading when the save button click', () => {
        const wrapper = shallow(<Tabs/>).dive();
        wrapper.find(Button).simulate('click');
        expect(wrapper.find(Button).prop('disabled'))
        .to.equal(true);
        expect(wrapper.state().loading).to.equal(true);
    });

    it('should change the save button view when button click', () => {
        var clock; 
        beforeEach(() => {
            const wrapper = shallow(<Tabs />).dive();
            wrapper.find(Button).simulate('click');
            clock = sinon.useFakeTimers();
        });
   
        it('the save button', () => {
            setTimeout( () =>  {
                expect(wrapper.state().loading).to.equal(true);
            }, 2000);
            clock.tick(2001);
            setTimeout( () =>  {
                expect(wrapper.state().success).to.equal(true);
            }, 3000);
            clock.tick(3001);
            expect(wrapper.state().success).to.equal(false);
        });
    })

    it('should appear snackbar when the save button click', () => {
        const wrapper = shallow(<Tabs/>).dive();
        wrapper.find(Button).simulate('click');
        wrapper.setState({open: true});
        expect(wrapper.state().open).to.equal(true);
    });

    it('should be active tab2 when tab1 is saved', () => {
        var clock; 
        beforeEach(() => {
            const wrapper = shallow(<Tabs />).dive();
            wrapper.find(Button).simulate('click');
            clock = sinon.useFakeTimers();        
        });
        it('tab2', () => {
            setTimeout( () =>  {
                expect(wrapper.state().disabled).to.equal(false);
            }, 2000);
            clock.tick(2001);
            expect(wrapper.state().tabValue).to.equal(2);

        });
    })

    it('should be active tab3 when tab2 is saved', () => {
        var clock; 
        beforeEach(() => {
            const wrapper = shallow(<Tabs />).dive();
            wrapper.find(Button).simulate('click');
            clock = sinon.useFakeTimers();        
        });
        it('tab2', () => {
            setTimeout( () =>  {
                expect(wrapper.state().disabled2).to.equal(false);
            }, 2000);
            clock.tick(2001);
            expect(wrapper.state().tabValue).to.equal(3);

        });
    })
})

describe('<Snackbar/>', () => {
    it('should have props for onClose, variant, message', () => {
        const handleClose = sinon.spy();
        const wrapper = shallow(<Snackbar
            onClose={handleClose}
            variant = "success"
            message="İşleminiz başarılı!"
        />);
        wrapper.props().variant.should.be.defined;
        wrapper.props().message.should.be.defined;
        wrapper.props().onClose.should.be.defined;
    });

    it('should close snackbar when the CloseIcon button click ', () =>{
        const handleClose = sinon.spy();
        const wrapper = mount(<Snackbar
            onClose={handleClose}
            variant = "success"
            message="İşleminiz başarılı!"
        />);
        wrapper.find(IconButton).simulate('click');
        expect(handleClose.calledOnce).to.equal(true);
    });
})


