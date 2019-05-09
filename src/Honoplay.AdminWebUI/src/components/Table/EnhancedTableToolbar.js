import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import {Toolbar, IconButton, 
        Tooltip, MuiThemeProvider, Typography} from '@material-ui/core';
import DeleteIcon from '@material-ui/icons/Delete';
import { Style, theme} from './Style';
  
const EnhancedTableToolbar = props => {
    const { numSelected, classes, handleDelete } = props;
    return (
      <MuiThemeProvider theme={theme}>
        <Toolbar className={classes.headRoot}>
          <div className={classes.headTitle}>
            {numSelected > 0 && (
              <Typography variant="subtitle1" className={classes.typography}>
                {numSelected} seçili
              </Typography>
            )}
          </div>
          <div className={classes.headSpacer} />
          <div>
            {numSelected > 0 && (
              <Tooltip title="Delete">
                <IconButton aria-label="Delete" onClick={handleDelete}>
                  <DeleteIcon/>
                </IconButton>
              </Tooltip>
            )}
          </div>
      </Toolbar>
      </MuiThemeProvider>
    );
  };

  export default withStyles(Style)(EnhancedTableToolbar);
