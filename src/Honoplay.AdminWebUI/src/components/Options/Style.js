
const Style = theme => ({
  root: {
    flexGrow: 1,
    padding: 8 * 3
  },
});

export default Style;

import {createMuiTheme} from '@material-ui/core';
import {deepPurple, deepOrange} from '@material-ui/core/colors';
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

