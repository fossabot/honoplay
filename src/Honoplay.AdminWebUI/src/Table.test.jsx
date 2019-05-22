import React from 'react';
import ReactDOM from 'react-dom';
import 'jsdom-global/register'; 
import should from 'should';
import { expect } from 'chai';
import sinon from 'sinon';
import { configure, shallow, mount, render } from 'enzyme';

import Table from './components/Table/TableComponent';
import TableToolbar from './components/Table/EnhancedTableToolbar';
import TableHead from './components/Table/EnhancedTableHead';
import TableMenu from './components/Table/TableMenu';

import {TableRow, IconButton} from '@material-ui/core';

import Adapter from 'enzyme-adapter-react-16';


configure({ adapter: new Adapter() });

describe('<Table/>', () => {
  it('should have props for data and columns', () => {
    const wrapper = shallow (<Table 
      data={[{'id':0, 'Name':'Şaduman', 'Surname':'Küçük', 'User Name': 'sadumankucuk'}]}                                
      columns={['Name','Surname','User Name',' ']}/>);
    wrapper.props().data.should.be.defined;
    wrapper.props().columns.should.be.defined; 
  });

  it('should call TableToolbar on TableRow(checkbox) click', () => {
    const wrapper = mount (<Table 
      data={[{'id':0, 'Name':'Şaduman', 'Surname':'Küçük', 'User Name': 'sadumankucuk'}]}                                  
      columns={['Name','Surname','User Name',' ']}/>);
    wrapper.find(TableRow).at(1).simulate('click');
    expect(wrapper.find(TableToolbar)).to.have.lengthOf(1);
  })

})

describe('<TableToolbar/>', () => {
  it('should have props for handleDelete and numSelected', () => {
    const handleDelete = sinon.spy();
    const wrapper = shallow (<TableToolbar 
      handleDelete={handleDelete} 
      numSelected={2}/>);
    wrapper.props().handleDelete.should.be.defined;
    wrapper.props().numSelected.should.be.defined; 
  })

  it('should call handleDelete() when clicked', () => {
    const handleDelete = sinon.spy();
    const wrapper = mount (<TableToolbar 
      handleDelete={handleDelete} 
      numSelected={2}/>);
    wrapper.find(IconButton).simulate('click');
    expect(handleDelete.calledOnce).to.equal(true)
  })

})

describe('<TableHead/>', () => {
  it('should have props for columns, numSelected, onSelectAllClick and rowCount', () => {
    const handleSelectAllClick = sinon.spy();
    const wrapper = shallow (<TableHead 
      columns={['Name','Surname','User Name',' ']} 
      numSelected={2} 
      onSelectAllClick={handleSelectAllClick} 
      rowCount={5}/>);
    wrapper.props().columns.should.be.defined;
    wrapper.props().numSelected.should.be.defined;
    wrapper.props().onSelectAllClick.should.be.defined;
    wrapper.props().rowCount.should.be.defined;
  })
})