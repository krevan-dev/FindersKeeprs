<template>
  <div class="modal" tabindex="-1">
    <div class="modal-dialog modal-dialog-centered modal-lg">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">
            <slot name="modal-title"></slot>
          </h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
            title="Close Window"
          ></button>
        </div>
        <div class="modal-body">
          <slot name="modal-body" />
        </div>
        <!-- <div class="modal-footer">
          <slot name="modal-footer" />
        </div> -->
      </div>
    </div>
  </div>
</template>