export const genderToString = (array) => {
    array.map(item => {
        if (item.gender == 0) {
            item.gender = "Erkek"
        } else item.gender = "Kadın"
    })
    return array;
}