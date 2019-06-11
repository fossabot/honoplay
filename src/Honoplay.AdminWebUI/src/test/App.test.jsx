import should from 'should';
import React from 'react';
import ReactDOM from 'react-dom';
import { configure, shallow } from 'enzyme';

import App from '../App.jsx';
import { Route } from 'react-router-dom';
import Layout from '../components/Layout/LayoutComponent';
import Questions from '../views/Questions/Questions';
import TenantInformation from '../views/TenantInformation/TenantInformation';

import Adapter from 'enzyme-adapter-react-16';

configure({ adapter: new Adapter() });

let pathMap = {};
describe('<App/>', () => {
    const component = shallow(<App/>);
    pathMap = component.find(Route).reduce((pathMap, route) => {
        const routeProps = route.props();
        pathMap[routeProps.path] = routeProps.component;
        return pathMap;
      }, {});
  it('Should redirect to Questions page', () => {
    (pathMap['/home/sorular']).should.equal(Questions);
  })
  it('Should redirect to Tenant Information page', () => {
    (pathMap['/home/sirketbilgileri']).should.equal(TenantInformation);
  })
})