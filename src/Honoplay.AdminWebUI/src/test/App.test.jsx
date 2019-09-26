import 'jsdom-global/register';
import React from 'react';
import { configure, shallow } from 'enzyme';
import configureStore from 'redux-mock-store';

import App from '../App.jsx';
import { renewToken } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/AdminUser';

//on the localstorage for token
global.window = {};
import 'mock-local-storage';
window.localStorage = global.localStorage;

import Adapter from 'enzyme-adapter-react-16';

configure({ adapter: new Adapter() });

describe('<App/>', () => {
  const mockStore = configureStore();
  const initialState = { renewToken };
  const store = mockStore(initialState);

  it('Should open App', () => {
    const wrapper = shallow(<App store={store} />);
  });
});
