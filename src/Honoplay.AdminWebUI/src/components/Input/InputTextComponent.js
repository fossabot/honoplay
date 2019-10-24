import React from 'react';
import { Grid, InputLabel, InputBase } from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles';
import { Style } from './Style';

const InputTextComponent = props => {
  const {
    classes,
    labelName,
    inputType,
    multiline,
    name,
    value,
    onChange,
    error,
    placeholder,
    htmlFor,
    id
  } = props;
  return (
    <div className={classes.inputRoot}>
      <Grid container spacing={3}>
        {labelName && (
          <Grid item xs={12} sm={2} className={classes.labelCenter}>
            <InputLabel
              htmlFor={htmlFor}
              className={classes.bootstrapFormLabel}
            >
              {labelName}
            </InputLabel>
          </Grid>
        )}
        <Grid item xs={12} sm={9}>
          <InputBase
            id={id}
            error={error ? true : false}
            placeholder={placeholder}
            onChange={onChange}
            multiline={multiline}
            name={name}
            value={value}
            type={inputType}
            fullWidth
            classes={{
              root: classes.bootstrapRoot,
              input: error ? classes.bootstrapError : classes.bootstrapInput
            }}
          />
        </Grid>
        <Grid item xs={12} sm={1} />
      </Grid>
    </div>
  );
};

export default withStyles(Style)(InputTextComponent);
