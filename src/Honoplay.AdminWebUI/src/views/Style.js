import {green} from '@material-ui/core/colors';
const Style = theme => ({
    root: {
      flexGrow: 1,
      padding: 8 * 3
    },
    linkStyle: {
      fontSize:16,
      color: '#673ab7',
      textDecorationLine: 'none',
      '&:active': {
       color: '#673ab7',
      },
    },
    buttonProgress: {
      color: green[500],
      position: 'absolute',
      marginTop: -30,
      marginLeft: 25
    },
    buttonSuccess: {
      backgroundColor: green[500],
      boxShadow: '0 .5rem 1rem rgba(0,128,0,1)',
      '&:hover': {
        backgroundColor: green[700],
      },

    },
});

export default Style;
