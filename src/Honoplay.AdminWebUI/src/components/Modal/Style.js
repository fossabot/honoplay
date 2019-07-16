import { deepPurple, deepOrange, green } from '@material-ui/core/colors';
import { createMuiTheme } from '@material-ui/core';

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
    paddingLeft: 20,
    paddingTop: 5
  },
  addProgress: {
    color: green[500],
    position: 'absolute',
    marginTop: -30,
    marginLeft: 18
  },
  loadingProgress: {
    color: green[500],
    position: 'absolute',
    marginTop: -20,
    marginLeft: 420
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
