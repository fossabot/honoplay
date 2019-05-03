export const Style = (theme) => ({
    modalPaper: {
      display: 'flex',
      justifyContent: 'left',
      flexWrap: 'wrap',
      padding: theme.spacing.unit / 2,
      paddingBottom: 8
    },
    contextDialog: {
      paddingTop: 35, 
      paddingBottom:35
    },
});
import {deepPurple, deepOrange} from '@material-ui/core/colors';
import {createMuiTheme} from '@material-ui/core';
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
