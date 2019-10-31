export const GetImage = avatarId => {
  const localStorageToken = localStorage.getItem("token");
  return `https://demo.honoplay.com/api/trainee/avatar/getfile/${avatarId}?access_token=${localStorageToken}`;
};
