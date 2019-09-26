import React from 'react';
import 'jsdom-global/register';
import { expect } from 'chai';
import { configure, shallow } from 'enzyme';

import Adapter from 'enzyme-adapter-react-16';
configure({ adapter: new Adapter() });

import CompanyCard from '../components/Layout/CompanyCard';
import List from '../components/Layout/ListItemComponent';
import Layout from '../components/Layout/LayoutComponent';

import { IconButton } from '@material-ui/core';

describe('<List/>', () => {
  it('Should have props  pageLink, pageIcon and pageName', () => {
    const wrapper = shallow(
      <List
        pageLink="/home/sorular"
        pageIcon="question-circle"
        pageName="Sorular"
      />
    );
    wrapper.props().pageLink.should.be.defined;
    wrapper.props().pageIcon.should.be.defined;
    wrapper.props().pageName.should.be.defined;
  });
});

describe('<Layout/>', () => {
  it('should open drawer when mobil menu icon click', () => {
    const wrapper = shallow(<Layout />).dive();
    wrapper.find(IconButton).simulate('click');
    wrapper.setState({ mobileOpen: true });
    expect(wrapper.state().mobileOpen).to.equal(true);
  });

  it('should open drawer when mobil menu icon click', () => {
    const wrapper = shallow(<Layout />).dive();
    expect(wrapper.find(CompanyCard).at(1)).to.have.length(1);
  });
});
