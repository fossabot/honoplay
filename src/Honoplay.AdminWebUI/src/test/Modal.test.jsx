// import React from 'react';
// import ReactDOM from 'react-dom';
// import 'jsdom-global/register'; 
// import should from 'should';
// import { expect } from 'chai';
// import sinon from 'sinon';
// import { configure, shallow, mount, render } from 'enzyme';
// import { init } from '@omegabigdata/terasu-api-proxy';

// import Adapter from 'enzyme-adapter-react-16';
// configure({ adapter: new Adapter() });

// import Modal from '../components/Modal/ModalComponent';

// import {FormGroup} from '@material-ui/core';

// describe('<Modal/>', () => {
//     it('should have props for data, modalTitle, modalInputName, handleClickClose ', () => {
//         const handleClickClose = sinon.spy();
//         const wrapper = shallow(<Modal
//             data = {[{'id':0, 'Name':'Intern'}]} 
//             modalTitle = 'Working Status Add'
//             modalInputName = 'Working Status'
//             handleClickClose = {handleClickClose}
//         />);
//         wrapper.props().data.should.be.defined;
//         wrapper.props().modalTitle.should.be.defined; 
//         wrapper.props().modalInputName.should.be.defined;
//         wrapper.props().handleClickClose.should.be.defined;
//     });

//     it('should have state for selectedValue', () => {
//         const handleClickClose = sinon.spy();
//         const wrapper = shallow(<Modal
//             data={[{'id':0, 'Name':'Intern'}]} 
//             modalTitle = 'Working Status Add'
//             modalInputName = 'Working Status'
//             handleClickClose = {handleClickClose}
//         />).dive();
//         expect(wrapper.state().selectedValue).to.equal('');
//     });

//     it('should change selectedValue when FormGoup (Radio button or label name) click', () => {
//         const handleClickClose = sinon.spy();
//         const wrapper = shallow(<Modal
//             data = {[{'id':0, 'Name':'Intern'}]} 
//             modalTitle = 'Working Status Add'
//             modalInputName = 'Working Status'
//             handleClickClose = {handleClickClose}
//         />).dive();
//         wrapper.find(FormGroup).simulate('click');
//         wrapper.setState({selectedValue : 'Intern'});
//         expect(wrapper.state().selectedValue).to.equal('Intern');
//     })
// })