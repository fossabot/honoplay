import {createMuiTheme} from '@material-ui/core';
import {deepPurple, deepOrange} from '@material-ui/core/colors';

export const Style = (theme) => ({
    button: {
        color: 'white',
        textTransform: 'capitalize',
    },
    buttonDiv: {
        paddingRight: 5,
    },
});

export const theme = createMuiTheme({
  palette: {
    primary: deepPurple,
    secondary: {
      main: deepOrange[300]
    }
  },
  typography: {
    useNextVariants: true,
  },
});
