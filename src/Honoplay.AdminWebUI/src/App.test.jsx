import should from 'should';
import React from 'react';
import ReactDOM from 'react-dom';
import { configure, shallow } from 'enzyme';

import App from './App.jsx';
import { Route } from 'react-router-dom';
import Layout from './components/Layout/LayoutComponent';
import Sorular from './views/Sorular/Sorular';
import SirketBilgileri from './views/SirketBilgileri/SirketBilgileri';

import Adapter from 'enzyme-adapter-react-16';

configure({ adapter: new Adapter() });

let pathMap = {};
describe('routes using array of routers', () => {

    const component = shallow(<App/>);
    pathMap = component.find(Route).reduce((pathMap, route) => {
        const routeProps = route.props();
        pathMap[routeProps.path] = routeProps.component;
        return pathMap;
      }, {});
  it(' Sorular ', () => {
    (pathMap['/home/sorular']).should.equal(Sorular);
  })
  it('Şirket Bilgileri ', () => {
    (pathMap['/home/sirketbilgileri']).should.equal(SirketBilgileri);
  })
})