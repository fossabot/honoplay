import {
    INCREMENT,
    DECREMENT,
    RESET
} from '../helpers/ActiveStepHelpers';

const initialState = {
    activeStep: 0,
}

function ActiveStepReducer(state = initialState, action) {
    switch (action.type) {
        case INCREMENT: {
            return { ...state, ...action }
        }
        case DECREMENT: {
            return { ...state, ...action }
        }
        case RESET: {
            return (initialState)
        }
        default:
            return state
    }
}

export default ActiveStepReducer;