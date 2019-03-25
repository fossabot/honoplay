import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import {IconButton,InputBase,Paper} from '@material-ui/core';
import SearchIcon from '@material-ui/icons/Search';
import SearchStyle from './SearchStyle';

class SearchInputComponent extends React.Component {
  render() {
    const { classes,InputId,PlaceHolderName} = this.props;

    return (
      <Paper className={classes.root} elevation={1}>
        <InputBase id={InputId}
                        placeholder={PlaceHolderName}
                        type="search"
                        fullWidth
                        classes={{
                          root: classes.bootstrapRoot,
                          input: classes.bootstrapInput,
                        }}
                />      
        <IconButton className={classes.iconButton} aria-label="Search">
          <SearchIcon />
        </IconButton>
      </Paper>
    );
  }
}

SearchInputComponent.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(SearchStyle)(SearchInputComponent);