export const Style = (theme) => ({
    modalPaper: {
      maxHeight: 200, 
      overflow: 'auto',
      display: 'flex',
      justifyContent: 'left',
      flexWrap: 'wrap',
      padding: theme.spacing.unit / 20,
      paddingBottom: 20
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
