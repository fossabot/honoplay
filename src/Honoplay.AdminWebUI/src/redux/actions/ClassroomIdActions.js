import {
    CHANGE_ID
} from '../helpers/ClassroomIdHelpers';

export const changeId = id => {
    const num = id
    return {
        type: CHANGE_ID,
        id: num
    }
}
