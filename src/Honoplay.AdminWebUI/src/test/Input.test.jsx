import React from 'react';
import ReactDOM from 'react-dom';
import 'jsdom-global/register'; 
import should from 'should';
import { expect } from 'chai';
import sinon from 'sinon';
import { configure, shallow, mount, render } from 'enzyme';
import { init } from '@omegabigdata/terasu-api-proxy';

import Adapter from 'enzyme-adapter-react-16';

import DropDownInput from '../components/Input/DropDownInputComponent';
import FileInput from '../components/Input/FileInputComponent';
import Input from '../components/Input/InputTextComponent';

import {IconButton, Modal, Button} from '@material-ui/core';

configure({ adapter: new Adapter() });

describe('<DropDownInput/>', () => {
    it('should have props for data and labelName', () => {
        const wrapper = shallow (<DropDownInput
            data={[{'id':0, 'Name':'Design'}]} 
            labelName= 'Departmant'   
        />);
        wrapper.props().data.should.be.defined;
        wrapper.props().labelName.should.be.defined; 
    });

    it('should have options if we have data', async () => {
        await init(3);
        const wrapper = shallow (<DropDownInput
            data={[{'id':0, 'Name':'Design'},
                   {'id':1, 'Name':'Software'}]} 
            labelName= 'Departmant'   
        />).dive();
        expect(wrapper.find('option').at(1,2).prop('value')).to.equal(0,1);
    })

    it('should open Modal when IconButton click', async () => {
        await init(3);
        const wrapper = mount (<DropDownInput
            data={[{'id':0, 'Name':'Intern'}]} 
            labelName= 'Working Status'
            describable
        />);
        wrapper.find(IconButton).simulate('click');
        expect(wrapper.find(Modal).prop('open')).to.equal(true);
    });
})

describe('<FileInput/>', () => {
    it('should have props for labelName', () => {
        const wrapper = shallow (<FileInput
            labelName= 'Tenant Logo'
        />);
        wrapper.props().labelName.should.be.defined;
    });

    it('should have filename cahnge when button is clicked ', () => {
        const wrapper = shallow (<FileInput
            labelName= "Tenant Logo"
        />).dive();
        wrapper.find(Button).simulate('click');
        wrapper.setState({filename: 'Image.jpeg'});
        expect(wrapper.state().filename).to.equal('Image.jpeg');
    })
})

describe('<Input/>', () => {
    it('should have props for labelName and inputType', () => {
        const wrapper = shallow (<Input
            labelName = 'Name'
            inputType = 'text'
        />);
        wrapper.props().labelName.should.be.defined;
        wrapper.props().inputType.should.be.defined; 
    })
})