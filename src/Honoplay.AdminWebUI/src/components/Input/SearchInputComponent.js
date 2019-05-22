import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import {IconButton,InputBase,Paper} from '@material-ui/core';
import SearchIcon from '@material-ui/icons/Search';
import {Style} from './Style';

class SearchInputComponent extends React.Component {
  render() {
    const { classes,PlaceHolderName} = this.props;

    return (
      <Paper className={classes.searchRoot} elevation={1}>
        <InputBase 
          placeholder={PlaceHolderName}
          type="search"
          fullWidth
          classes={{
            root: classes.bootstrapRoot,
            input: classes.bootstrapInput,
          }}
        />      
        <IconButton 
          className={classes.searchIconButton} aria-label="Search">
          <SearchIcon />
        </IconButton>
      </Paper>
    );
  }
}

export default withStyles(Style)(SearchInputComponent);