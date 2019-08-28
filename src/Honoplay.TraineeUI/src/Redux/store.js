import { addReducerToList } from '@omegabigdata/honoplay-redux-helper/dist/Src/reducers/index';
import { createStore, applyMiddleware, compose } from 'redux';
import thunk from 'redux-thunk';
const newReducers = addReducerToList({
    
});
const store = createStore(newReducers, {}, compose(applyMiddleware(thunk)));

export default store;