import {
    CHANGE_ID
} from '../helpers/ClassroomIdHelpers';

const initialState = {
    id: 0,
}

function ClassroomIdReducer(state = initialState, action) {
    switch (action.type) {
        case CHANGE_ID: {
            return { ...state, ...action }
        }
        default:
            return state
    }
}

export default ClassroomIdReducer;