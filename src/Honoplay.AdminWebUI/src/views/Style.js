import {green, deepPurple, amber} from '@material-ui/core/colors';
const Style = theme => ({
    root: {
      flexGrow: 1,
      padding: 8 * 6
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
      marginLeft: 18
    },
});

export default Style;
