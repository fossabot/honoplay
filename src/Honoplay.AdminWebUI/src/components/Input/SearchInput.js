import React from 'react';
import { InputBase } from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles';
import SearchIcon from '@material-ui/icons/Search';
import {Style} from './Style';

const SearchInput = (props) => {
  const { classes } = props;
  return (
    <div>
      <div className={classes.searchRoot}>
        <div className={classes.searchGrow} />
            <div className={classes.search}>
              <div className={classes.searchIcon}>
                <SearchIcon />
              </div>
              <InputBase
                placeholder="Ara…"
                classes={{
                  root: classes.searchInputRoot,
                  input: classes.searchInput,
                }}
              />
        </div>
      </div>
    </div>
  );
}

export default withStyles(Style)(SearchInput);