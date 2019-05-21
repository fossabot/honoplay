import {FETCH_SORULAR} from '../actions/sorular';

const initialState = {
	sorular: {},
};

export default (state = initialState, action) => {
    switch (action.type){
        case FETCH_SORULAR:
            return {...state};
        default:
            return state;
    }
}