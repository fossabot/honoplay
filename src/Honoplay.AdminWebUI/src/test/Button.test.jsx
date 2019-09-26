import React from 'react';
import 'jsdom-global/register';
import should from 'should';
import { expect } from 'chai';
import sinon from 'sinon';
import { configure, shallow } from 'enzyme';

import Adapter from 'enzyme-adapter-react-16';
configure({ adapter: new Adapter() });

import ButtonComponent from '../components/Button/ButtonComponent';
import { Button } from '@material-ui/core';

describe('<Button/>', () => {
  it('should have props for data, buttonColor, buttonName, onClick ', () => {
    const onClick = sinon.spy();
    const wrapper = shallow(
      <ButtonComponent
        buttonColor="primary"
        buttonName="Save"
        onClick={onClick}
      />
    );
    wrapper.props().buttonColor.should.be.defined;
    wrapper.props().buttonName.should.be.defined;
    wrapper.props().onClick.should.be.defined;
  });

  it('should invoke onClick callback when click to "Button"', () => {
    const onClick = sinon.spy();
    const wrapper = shallow(
      <ButtonComponent
        buttonColor="primary"
        buttonName="Save"
        onClick={onClick}
      />
    ).dive();
    wrapper.find(Button).simulate('click');
    expect(onClick.calledOnce).to.equal(true);
  });

  it('should not do anything when clicked because the button is disabled', () => {
    const onClick = sinon.spy();
    const wrapper = shallow(
      <ButtonComponent
        buttonColor="primary"
        buttonName="Save"
        onClick={onClick}
        disabled
      />
    ).dive();
    wrapper.find(Button).simulate('click');
    expect(wrapper.find(Button).prop('disabled')).to.equal(true);
  });
});
