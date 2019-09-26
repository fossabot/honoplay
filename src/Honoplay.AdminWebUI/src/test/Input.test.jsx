import React from 'react';
import 'jsdom-global/register';
import { expect } from 'chai';
import sinon from 'sinon';
import { configure, shallow } from 'enzyme';
import { init } from '@omegabigdata/terasu-api-proxy';

import Adapter from 'enzyme-adapter-react-16';
configure({ adapter: new Adapter() });

import DropDownInput from '../components/Input/DropDownInputComponent';
import ImageInput from '../components/Input/ImageInputComponent';
import Input from '../components/Input/InputTextComponent';
import Modal from '../components/Modal/ModalComponent';

import { IconButton, Button, InputBase } from '@material-ui/core';

describe('<DropDownInput/>', () => {
  it('should have props for data and labelName', () => {
    const wrapper = shallow(
      <DropDownInput
        data={[{ id: 0, Name: 'Tasarım' }]}
        labelName="Departmant"
      />
    );
    wrapper.props().data.should.be.defined;
    wrapper.props().labelName.should.be.defined;
  });

  it('should have options if we have data', async () => {
    await init(3);
    const wrapper = shallow(
      <DropDownInput
        data={[{ id: 0, Name: 'Tasarım' }, { id: 1, Name: 'Yazılım' }]}
        labelName="Departmant"
      />
    ).dive();
    expect(
      wrapper
        .find('option')
        .at(1, 2)
        .prop('value')
    ).to.equal(0, 1);
  });

  it('should open Modal when IconButton click', async () => {
    await init(3);
    const wrapper = shallow(
      <DropDownInput
        data={[{ id: 0, Name: 'Stajyer' }]}
        labelName="Working Status"
        describable
      />
    ).dive();
    wrapper.find(IconButton).simulate('click');
    expect(wrapper.find(Modal).prop('open')).to.equal(true);
  });
});

describe('<ImageInput/>', () => {
  it('should have props for labelName', async () => {
    await init(3);
    const wrapper = shallow(<ImageInput labelName="Tenant Logo" />);
    wrapper.props().labelName.should.be.defined;
  });

  it('should have filename cahnge when button is clicked ', () => {
    const wrapper = shallow(<ImageInput labelName="Tenant Logo" />).dive();
    wrapper.find(Button).simulate('click');
    wrapper.setState({ filename: 'Image.jpeg' });
    expect(wrapper.state().filename).to.equal('Image.jpeg');
  });
});

describe('<Input/>', () => {
  it('should have props for labelName and onChange', () => {
    const onChange = sinon.spy();
    const wrapper = shallow(<Input inputType="text" onChange={onChange} />);
    wrapper.props().inputType.should.be.defined;
    wrapper.props().onChange.should.be.defined;
  });

  it('should be of type `function` onChange ', () => {
    const onChange = sinon.spy();
    const wrapper = shallow(<Input inputType="text" onChange={onChange} />);
    expect(typeof wrapper.props().onChange === 'function').to.equal(true);
  });

  it('should display an error when error', () => {
    const onChange = sinon.spy();
    const wrapper = shallow(
      <Input inputType="text" onChange={onChange} error />
    ).dive();
    expect(wrapper.find(InputBase).prop('error')).to.equal(true);
  });

  it('should display an placeholder if input has placeholder', () => {
    const onChange = sinon.spy();
    const wrapper = shallow(
      <Input inputType="text" onChange={onChange} placeholder="name" />
    ).dive();
    expect(wrapper.find(InputBase).prop('placeholder')).to.equal('name');
  });
});
