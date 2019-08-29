import { addReducerToList } from '@omegabigdata/honoplay-redux-helper/dist/Src/reducers/index';
import { createStore, applyMiddleware, compose } from 'redux';
import thunk from 'redux-thunk';
import ActiveStep from './reducer/ActiveStepReducer';
import ClassroomId from './reducer/ClassroomIdReducer';


const newReducers = addReducerToList({
    ActiveStep,
    ClassroomId
});

const store = createStore(newReducers, {}, compose(applyMiddleware(thunk)));

export default store;