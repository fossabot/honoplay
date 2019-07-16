export const genderToString = (array) => {
    array.map(item => {
        if (item.gender == 0) {
            item.gender = "Erkek"
        } else item.gender = "Kadın"
    })
    return array;
}

export const departmentToString = (departmentArray, array) => {
    array.map(item => {
        departmentArray.map(department => {
            if( item.departmentId === department.id) {
                item.departmentId = department.name;
            }
        })
    })
}