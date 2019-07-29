import React from 'react';
import ReactDOM from 'react-dom';
import 'jsdom-global/register';
import should from 'should';
import { expect } from 'chai';
import sinon from 'sinon';
import { configure, shallow, mount } from 'enzyme';
import { init } from '@omegabigdata/terasu-api-proxy';

import Adapter from 'enzyme-adapter-react-16';
configure({ adapter: new Adapter() });

import Table from '../components/Table/TableComponent';
import TableToolbar from '../components/Table/EnhancedTableToolbar';
import TableHead from '../components/Table/EnhancedTableHead';
import Modal from '../components/Modal/ModalComponent';

import {
  TableRow, IconButton, Typography,
  Checkbox
} from '@material-ui/core';


describe('<Table/>', () => {
  it('should have props for data and columns', () => {
    const wrapper = shallow(<Table
      data={[{ 'id': 0, 'Name': 'Şaduman', 'Surname': 'Küçük', 'User Name': 'sadumankucuk' }]}
      columns={['Name', 'Surname', 'User Name', ' ']} />);
    wrapper.props().data.should.be.defined;
    wrapper.props().columns.should.be.defined;
  });

  it('should have state for page and rowsPerPage', async () => {
    await init(3);
    const wrapper = shallow(<Table
      data={[{ 'id': 0, 'Name': 'Şaduman', 'Surname': 'Küçük', 'User Name': 'sadumankucuk' }]}
      columns={['Name', 'Surname', 'User Name', ' ']} />).dive();
    expect(wrapper.state().page).to.equal(0);
    expect(wrapper.state().rowsPerPage).to.equal(5);
  });

  it('should call TableToolbar on TableRow(checkbox) click', () => {
    const wrapper = mount(<Table
      data={[{ 'id': 0, 'Name': 'Şaduman', 'Surname': 'Küçük', 'User Name': 'sadumankucuk' }]}
      columns={['Name', 'Surname', 'User Name', ' ']} />);
    wrapper.find(TableRow).at(1).simulate('click');
    expect(wrapper.find(TableToolbar)).to.have.lengthOf(1);

  })

  it('should call Edit Button on TableRow(checkbox) click', () => {
    const wrapper = shallow(<Table
      data={[{ 'id': 0, 'Name': 'Şaduman', 'Surname': 'Küçük', 'User Name': 'sadumankucuk' }]}
      columns={['Name', 'Surname', 'User Name', ' ']} />).dive();
    wrapper.find(TableRow).at(1).simulate('click');
    wrapper.setState({ selected: [0] });
    expect(wrapper.find(IconButton)).to.have.lengthOf(1);
  })

  it('should call modal on Edit Button click', () => {
    const wrapper = shallow(<Table
      data={[{ 'id': 0, 'Name': 'Şaduman', 'Surname': 'Küçük', 'User Name': 'sadumankucuk' }]}
      columns={['Name', 'Surname', 'User Name', ' ']} />).dive();
    wrapper.find(TableRow).at(1).simulate('click');
    wrapper.setState({ selected: [0] });
    wrapper.find(IconButton).simulate('click');
    expect(wrapper.find(Modal)).to.have.lengthOf(1);
  })
})

describe('<TableToolbar/>', () => {
  it('should have props for handleDelete and numSelected', () => {
    const handleDelete = sinon.spy();
    const wrapper = shallow(<TableToolbar
      handleDelete={handleDelete}
      numSelected={2} />);
    wrapper.props().handleDelete.should.be.defined;
    wrapper.props().numSelected.should.be.defined;
  })

  it('should call handleDelete() when clicked', () => {
    const handleDelete = sinon.spy();
    const wrapper = mount(<TableToolbar
      handleDelete={handleDelete}
      numSelected={2} />);
    wrapper.find(IconButton).simulate('click');
    expect(handleDelete.calledOnce).to.equal(true);
  })

  it('should call Typography on TableRow(checkbox) click ', () => {
    const handleDelete = sinon.spy();
    const wrapper = mount(<TableToolbar
      handleDelete={handleDelete}
      numSelected={3} />);
    wrapper.find(Typography);
    expect(wrapper.text()).to.equal('3 Seçili');
  })
})

describe('<TableHead/>', () => {
  it('should have props for columns, numSelected, onSelectAllClick and rowCount', () => {
    const handleSelectAllClick = sinon.spy();
    const wrapper = shallow(<TableHead
      columns={['Name', 'Surname', 'User Name', ' ']}
      numSelected={2}
      onSelectAllClick={handleSelectAllClick}
      rowCount={5} />);
    wrapper.props().columns.should.be.defined;
    wrapper.props().numSelected.should.be.defined;
    wrapper.props().onSelectAllClick.should.be.defined;
    wrapper.props().rowCount.should.be.defined;
  })

  it('should select all data when Checkbox click', () => {
    const handleSelectAllClick = sinon.spy();
    const wrapper = shallow(<TableHead
      columns={['Name', 'Surname', 'User Name', ' ']}
      numSelected={5}
      onSelectAllClick={handleSelectAllClick}
      rowCount={5} />).dive();
    expect(wrapper.find(Checkbox).prop('checked')).to.equal(true);
  })
})

