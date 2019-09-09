import should from "should";
import React from 'react';
import ReactDOM from 'react-dom';
import { configure, shallow } from 'enzyme';

import App from '../App.jsx';
import { Route } from 'react-router-dom';
import Questions from '../views/Questions/Questions';
import TenantInformation from '../views/TenantInformation/TenantInformation';
import Trainers from "../views/Trainers/Trainers";
import UserManagement from "../views/UserManagement/UserManagement";
import NewQuestion from "../views/Questions/NewQuestion";

// on the localstorage for token
global.window = {}
import 'mock-local-storage'
window.localStorage = global.localStorage

import Adapter from 'enzyme-adapter-react-16';

configure({ adapter: new Adapter() });

let pathMap = {};
describe('<App/>', () => {
  const component = shallow(<App />);
  pathMap = component.find(Route).reduce((pathMap, route) => {
    const routeProps = route.props();
    pathMap[routeProps.path] = routeProps.component;
    return pathMap;
  }, {});

  it('Should redirect to Questions page', () => {
    (pathMap['/honoplay/questions']).should.equal(Questions);
  })

  it('Should redirect to Tenant Information page', () => {
    (pathMap['/honoplay/tenantinformation']).should.equal(TenantInformation);
  })

  it('Should redirect to Trainers page', () => {
    (pathMap['/honoplay/trainers']).should.equal(Trainers);
  })

  it('Should redirect to User Management page', () => {
    (pathMap['/honoplay/usermanagement']).should.equal(UserManagement);
  })

  it('Should redirect to New Question page', () => {
    (pathMap['/honoplay/addquestion']).should.equal(NewQuestion);
  })
  
})